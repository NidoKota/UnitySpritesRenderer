struct uvTransform
{
    float2 position;
    float rotation;
    float2 scale;
};

static float2 rotate(const float2 uv, const float angle, const float2 pivot)
{
    const float angSin = sin(angle);
    const float angCos = cos(angle);

    const float2x2 mat = float2x2(angCos, -angSin, angSin, angCos);

    return mul(mat, uv - pivot) + pivot;
}

static float remap(const float val, const float inMin, const float inMax, const float outMin, const float outMax)
{
    return clamp(outMin + (val - inMin) * (outMax - outMin) / (inMax - inMin), outMin, outMax);
}

float2 getRightUpUV(const uvTransform transform)
{
    return transform.position + (transform.scale / 2.f);
}

float2 getLeftDownUV(const uvTransform transform)
{
    return transform.position - (transform.scale / 2.f);
}

float2 getInTransformUV(const uvTransform transform, const float2 position)
{
    const float2 rightUpUV = getRightUpUV(transform);
    const float2 leftDownUV = getLeftDownUV(transform);

    const float2 rotatedPosition = rotate(position, -transform.rotation, transform.position);
    
    return float2(
        remap(rotatedPosition.x, leftDownUV.x, rightUpUV.x, 0, 1),
        remap(rotatedPosition.y, leftDownUV.y, rightUpUV.y, 0, 1));
}

bool isInTransform(const uvTransform transform, const float2 position)
{
    const float2 rightUpUVabs = transform.position + abs(transform.scale / 2.f);
    const float2 leftDownUVabs = transform.position - abs(transform.scale / 2.f);

    const float2 rotatedPosition = rotate(position, -transform.rotation, transform.position);

    const float isInX = step(rotatedPosition.x, rightUpUVabs.x) * step(leftDownUVabs.x, rotatedPosition.x);
    const float isInY = step(rotatedPosition.y, rightUpUVabs.y) * step(leftDownUVabs.y, rotatedPosition.y);

    return isInX * isInY;
}