defmodule Documenso.AnonymousVerificationToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t()
        }

  schema "AnonymousVerificationToken" do
    field(:id, :string, primary_key: true)
    field(:token, :string)
    field(:expires_at, Documenso.PrismaDateTime, source: :expiresAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end