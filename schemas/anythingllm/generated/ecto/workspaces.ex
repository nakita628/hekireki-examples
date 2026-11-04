defmodule AnythingLLM.Workspaces do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          slug: String.t(),
          vector_tag: String.t() | nil,
          open_ai_temp: float() | nil,
          open_ai_history: integer(),
          last_updated_at: DateTime.t(),
          open_ai_prompt: String.t() | nil,
          similarity_threshold: float() | nil,
          chat_provider: String.t() | nil,
          chat_model: String.t() | nil,
          top_n: integer() | nil,
          chat_mode: String.t() | nil,
          pfp_filename: String.t() | nil,
          agent_provider: String.t() | nil,
          agent_model: String.t() | nil,
          query_refusal_response: String.t() | nil,
          vector_search_mode: String.t() | nil,
          router_id: integer() | nil,
          inserted_at: DateTime.t(),
          workspace_users: [AnythingLLM.WorkspaceUsers.t()],
          documents: [AnythingLLM.WorkspaceDocuments.t()],
          workspace_suggested_messages: [AnythingLLM.WorkspaceSuggestedMessages.t()],
          embed_configs: [AnythingLLM.EmbedConfigs.t()],
          threads: [AnythingLLM.WorkspaceThreads.t()],
          workspace_agent_invocations: [AnythingLLM.WorkspaceAgentInvocations.t()],
          prompt_history: [AnythingLLM.PromptHistory.t()],
          workspace_parsed_files: [AnythingLLM.WorkspaceParsedFiles.t()],
          memories: [AnythingLLM.Memories.t()]
        }

  schema "workspaces" do
    field(:name, :string)
    field(:slug, :string)
    field(:vector_tag, :string, source: :vectorTag)
    field(:open_ai_temp, :float, source: :openAiTemp)
    field(:open_ai_history, :integer, default: 20, source: :openAiHistory)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:open_ai_prompt, :string, source: :openAiPrompt)
    field(:similarity_threshold, :float, default: 0.25, source: :similarityThreshold)
    field(:chat_provider, :string, source: :chatProvider)
    field(:chat_model, :string, source: :chatModel)
    field(:top_n, :integer, default: 4, source: :topN)
    field(:chat_mode, :string, default: "chat", source: :chatMode)
    field(:pfp_filename, :string, source: :pfpFilename)
    field(:agent_provider, :string, source: :agentProvider)
    field(:agent_model, :string, source: :agentModel)
    field(:query_refusal_response, :string, source: :queryRefusalResponse)
    field(:vector_search_mode, :string, default: "default", source: :vectorSearchMode)
    field(:router_id, :integer)
    has_many(:workspace_users, AnythingLLM.WorkspaceUsers, foreign_key: :workspace_id)
    has_many(:documents, AnythingLLM.WorkspaceDocuments, foreign_key: :workspace_id)
    has_many(:workspace_suggested_messages, AnythingLLM.WorkspaceSuggestedMessages, foreign_key: :workspace_id)
    has_many(:embed_configs, AnythingLLM.EmbedConfigs, foreign_key: :workspace_id)
    has_many(:threads, AnythingLLM.WorkspaceThreads, foreign_key: :workspace_id)
    has_many(:workspace_agent_invocations, AnythingLLM.WorkspaceAgentInvocations, foreign_key: :workspace_id)
    has_many(:prompt_history, AnythingLLM.PromptHistory, foreign_key: :workspace_id)
    has_many(:workspace_parsed_files, AnythingLLM.WorkspaceParsedFiles, foreign_key: :workspace_id)
    has_many(:memories, AnythingLLM.Memories, foreign_key: :workspace_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end