defmodule Hoppscotch.VerificationToken do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          device_identifier: String.t(),
          token: String.t(),
          expires_on: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "VerificationToken" do
    field(:device_identifier, :string, source: :deviceIdentifier)
    field(:token, :string, primary_key: true)
    field(:expires_on, Hoppscotch.PrismaDateTime, source: :expiresOn)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
  end
end