defmodule AnythingLLM.EmbedConfigs do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          enabled: boolean(),
          chat_mode: String.t(),
          allowlist_domains: String.t() | nil,
          allow_model_override: boolean(),
          allow_temperature_override: boolean(),
          allow_prompt_override: boolean(),
          max_chats_per_day: integer() | nil,
          max_chats_per_session: integer() | nil,
          message_limit: integer() | nil,
          created_by: integer() | nil,
          inserted_at: DateTime.t(),
          workspace: AnythingLLM.Workspaces.t() | nil,
          users: AnythingLLM.Users.t() | nil,
          embed_chats: [AnythingLLM.EmbedChats.t()]
        }

  schema "embed_configs" do
    field(:uuid, :string)
    field(:enabled, :boolean, default: false)
    field(:chat_mode, :string, default: "query")
    field(:allowlist_domains, :string)
    field(:allow_model_override, :boolean, default: false)
    field(:allow_temperature_override, :boolean, default: false)
    field(:allow_prompt_override, :boolean, default: false)
    field(:max_chats_per_day, :integer)
    field(:max_chats_per_session, :integer)
    field(:message_limit, :integer, default: 20)
    field(:created_by, :integer, source: :createdBy)
    field(:users_id, :id, source: :usersId)
    belongs_to(:workspace, AnythingLLM.Workspaces, foreign_key: :workspace_id)
    belongs_to(:users, AnythingLLM.Users, foreign_key: :users_id, define_field: false)
    has_many(:embed_chats, AnythingLLM.EmbedChats, foreign_key: :embed_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end