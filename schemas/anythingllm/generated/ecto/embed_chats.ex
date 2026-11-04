defmodule AnythingLLM.EmbedChats do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          prompt: String.t(),
          response: String.t(),
          session_id: String.t(),
          include: boolean(),
          connection_information: String.t() | nil,
          inserted_at: DateTime.t(),
          embed_config: AnythingLLM.EmbedConfigs.t() | nil,
          users: AnythingLLM.Users.t() | nil
        }

  schema "embed_chats" do
    field(:prompt, :string)
    field(:response, :string)
    field(:session_id, :string)
    field(:include, :boolean, default: true)
    field(:connection_information, :string)
    field(:users_id, :id, source: :usersId)
    belongs_to(:embed_config, AnythingLLM.EmbedConfigs, foreign_key: :embed_id)
    belongs_to(:users, AnythingLLM.Users, foreign_key: :users_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end