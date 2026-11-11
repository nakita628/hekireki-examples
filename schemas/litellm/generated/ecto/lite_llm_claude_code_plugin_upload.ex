defmodule LiteLLM.LiteLLMClaudeCodePluginUpload do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          content: binary(),
          plugin: LiteLLM.LiteLLMClaudeCodePluginTable.t() | nil
        }

  schema "LiteLLM_ClaudeCodePluginUpload" do
    field(:content, :binary)
    belongs_to(:plugin, LiteLLM.LiteLLMClaudeCodePluginTable, foreign_key: :plugin_name, type: :string, references: :name)
  end
end