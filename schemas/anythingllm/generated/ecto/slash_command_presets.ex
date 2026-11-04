defmodule AnythingLLM.SlashCommandPresets do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          command: String.t(),
          prompt: String.t(),
          description: String.t(),
          uid: integer(),
          last_updated_at: DateTime.t(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "slash_command_presets" do
    field(:command, :string)
    field(:prompt, :string)
    field(:description, :string)
    field(:uid, :integer, default: 0)
    field(:last_updated_at, AnythingLLM.PrismaDateTime, autogenerate: true, source: :lastUpdatedAt)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end