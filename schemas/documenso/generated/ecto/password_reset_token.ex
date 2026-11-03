defmodule Documenso.PasswordResetToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          token: String.t(),
          expiry: DateTime.t(),
          inserted_at: DateTime.t(),
          user: Documenso.User.t() | nil
        }

  schema "PasswordResetToken" do
    field(:token, :string)
    field(:expiry, Documenso.PrismaDateTime)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Documenso.User, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end