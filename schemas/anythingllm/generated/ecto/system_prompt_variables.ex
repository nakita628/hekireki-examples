defmodule AnythingLLM.SystemPromptVariables do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          key: String.t(),
          value: String.t() | nil,
          description: String.t() | nil,
          type: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "system_prompt_variables" do
    field(:key, :string)
    field(:value, :string)
    field(:description, :string)
    field(:type, :string, default: "system")
    field(:user_id, :id, source: :userId)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end