defmodule Rallly.SpaceMemberInvite do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t(),
          role: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          invited_by: Rallly.User.t() | nil,
          space: Rallly.Space.t() | nil
        }

  schema "space_member_invites" do
    field(:email, :string)
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER], default: :MEMBER)
    belongs_to(:invited_by, Rallly.User, foreign_key: :inviter_id, type: :string)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id)
    timestamps(inserted_at_source: :created_at)
  end
end