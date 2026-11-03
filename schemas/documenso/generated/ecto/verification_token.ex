defmodule Documenso.VerificationToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          secondary_id: String.t(),
          identifier: String.t(),
          token: String.t(),
          completed: boolean(),
          expires: DateTime.t(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          user: Documenso.User.t() | nil
        }

  schema "VerificationToken" do
    field(:secondary_id, :string, source: :secondaryId)
    field(:identifier, :string)
    field(:token, :string)
    field(:completed, :boolean, default: false)
    field(:expires, Documenso.PrismaDateTime)
    field(:metadata, :map)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end