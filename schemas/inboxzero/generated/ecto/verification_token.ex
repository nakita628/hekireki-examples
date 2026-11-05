defmodule InboxZero.VerificationToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "VerificationToken" do
    field(:id, :string, primary_key: true)
    field(:identifier, :string)
    field(:token, :string)
    field(:expires, InboxZero.PrismaDateTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end