defmodule AnythingLLM.RecoveryCodes do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          code_hash: String.t(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "recovery_codes" do
    field(:code_hash, :string)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end