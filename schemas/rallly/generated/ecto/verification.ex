defmodule Rallly.Verification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          identifier: String.t(),
          value: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "verifications" do
    field(:id, :string, primary_key: true)
    field(:identifier, :string)
    field(:value, :string)
    field(:expires_at, Rallly.PrismaDateTime)
    timestamps(inserted_at_source: :created_at)
  end
end