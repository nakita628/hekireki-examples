defmodule LiteLLM.LiteLLMClaudeCodePluginDownload do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          downloaded_at: DateTime.t(),
          version: String.t() | nil,
          digest: String.t(),
          authenticated: boolean(),
          user_id: String.t() | nil,
          user_email: String.t() | nil,
          key_alias: String.t() | nil,
          team_id: String.t() | nil,
          team_alias: String.t() | nil,
          plugin: LiteLLM.LiteLLMClaudeCodePluginTable.t() | nil
        }

  schema "LiteLLM_ClaudeCodePluginDownload" do
    field(:downloaded_at, LiteLLM.PrismaDateTime, autogenerate: true)
    field(:version, :string)
    field(:digest, :string)
    field(:authenticated, :boolean)
    field(:user_id, :string)
    field(:user_email, :string)
    field(:key_alias, :string)
    field(:team_id, :string)
    field(:team_alias, :string)
    belongs_to(:plugin, LiteLLM.LiteLLMClaudeCodePluginTable, foreign_key: :plugin_id, type: :binary_id)
  end
end