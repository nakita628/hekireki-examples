defmodule Hoppscotch.InvitedUsers do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          admin_email: String.t(),
          invitee_email: String.t(),
          invited_on: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "InvitedUsers" do
    field(:admin_email, :string, source: :adminEmail)
    field(:invitee_email, :string, primary_key: true, source: :inviteeEmail)
    field(:invited_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :invitedOn)
    field(:admin_uid, :string, source: :adminUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :admin_uid, define_field: false, type: :string, references: :uid)
  end
end