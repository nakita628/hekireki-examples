defmodule LiteLLM.LiteLLMClaudeCodePluginVersion do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          version: String.t() | nil,
          description: String.t() | nil,
          manifest_json: String.t() | nil,
          published_at: DateTime.t() | nil,
          published_by: String.t() | nil,
          archive_available: boolean(),
          content: binary() | nil,
          plugin: LiteLLM.LiteLLMClaudeCodePluginTable.t() | nil
        }

  schema "LiteLLM_ClaudeCodePluginVersion" do
    field(:version, :string)
    field(:description, :string)
    field(:manifest_json, :string)
    field(:published_at, LiteLLM.PrismaDateTime)
    field(:published_by, :string)
    field(:archive_available, :boolean, default: false)
    field(:content, :binary)
    belongs_to(:plugin, LiteLLM.LiteLLMClaudeCodePluginTable, foreign_key: :plugin_id)
  end
end