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
}
