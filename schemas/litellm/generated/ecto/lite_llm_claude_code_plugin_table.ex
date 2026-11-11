defmodule LiteLLM.LiteLLMClaudeCodePluginTable do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          version: String.t() | nil,
          description: String.t() | nil,
          manifest_json: String.t() | nil,
          files_json: String.t() | nil,
          enabled: boolean(),
          created_by: String.t() | nil,
          anonymous_download_count: integer(),
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          uploaded_archive: LiteLLM.LiteLLMClaudeCodePluginUpload.t() | nil,
          versions: [LiteLLM.LiteLLMClaudeCodePluginVersion.t()],
          downloads: [LiteLLM.LiteLLMClaudeCodePluginDownload.t()]
        }

  schema "LiteLLM_ClaudeCodePluginTable" do
    field(:name, :string)
    field(:version, :string)
    field(:description, :string)
    field(:manifest_json, :string)
    field(:files_json, :string, default: "{}")
    field(:enabled, :boolean, default: true)
    field(:created_by, :string)
    field(:anonymous_download_count, :integer, default: 0)
    has_one(:uploaded_archive, LiteLLM.LiteLLMClaudeCodePluginUpload, foreign_key: :plugin_name, references: :name)
    has_many(:versions, LiteLLM.LiteLLMClaudeCodePluginVersion, foreign_key: :plugin_id)
    has_many(:downloads, LiteLLM.LiteLLMClaudeCodePluginDownload, foreign_key: :plugin_id)
    timestamps(inserted_at_source: :created_at)
  end
end