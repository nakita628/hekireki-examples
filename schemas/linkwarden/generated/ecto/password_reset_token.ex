defmodule Linkwarden.PasswordResetToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "PasswordResetToken" do
    field(:identifier, :string)
    field(:token, :string, primary_key: true)
    field(:expires, Linkwarden.PrismaDateTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end