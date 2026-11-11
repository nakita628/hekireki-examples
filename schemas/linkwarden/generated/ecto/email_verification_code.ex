defmodule Linkwarden.EmailVerificationCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Linkwarden.PrismaDateTime, autogenerate: {Linkwarden.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          identifier: String.t(),
          code: String.t(),
          attempts: integer(),
          expires: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "EmailVerificationCode" do
    field(:identifier, :string, primary_key: true)
    field(:code, :string)
    field(:attempts, :integer, default: 0)
    field(:expires, Linkwarden.PrismaDateTime)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end