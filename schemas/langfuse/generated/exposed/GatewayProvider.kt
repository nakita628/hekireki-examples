package langfuse.models

enum class GatewayProvider(
    val dbName: String,
) {
    OPENAI("openai"),
    ANTHROPIC("anthropic"),
    BEDROCK("bedrock"),
    OPENROUTER("openrouter"),
    XAI("xai"),
    VERCEL("vercel"),
    AZURE("azure"),
    GOOGLE_AI_STUDIO("google_ai_studio"),
}
