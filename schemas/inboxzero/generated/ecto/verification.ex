defmodule InboxZero.Verification do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          identifier: String.t(),
          value: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "verification" do
    field(:id, :string, primary_key: true)
    field(:identifier, :string)
    field(:value, :string)
    field(:expires_at, InboxZero.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end