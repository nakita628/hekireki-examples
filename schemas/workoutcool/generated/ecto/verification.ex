defmodule WorkoutCool.Verification do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          identifier: String.t(),
          value: String.t(),
          expires_at: DateTime.t(),
          created_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "verification" do
    field(:id, :string, primary_key: true)
    field(:identifier, :string)
    field(:value, :string)
    field(:expires_at, WorkoutCool.PrismaDateTime, source: :expiresAt)
    field(:created_at, WorkoutCool.PrismaDateTime, source: :createdAt)
    field(:updated_at, WorkoutCool.PrismaDateTime, source: :updatedAt)
  end
end