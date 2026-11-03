defmodule Formbricks.PasswordResetToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          token_hash: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Formbricks.User.t() | nil
        }

  schema "PasswordResetToken" do
    field(:id, :string, primary_key: true)
    field(:token_hash, :string)
    field(:expires_at, Formbricks.PrismaDateTime)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end