defmodule Formbricks.Invite do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Formbricks.PrismaDateTime, autogenerate: {Formbricks.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t(),
          name: String.t() | nil,
          expires_at: DateTime.t(),
          role: atom(),
          team_ids: [String.t()],
          inserted_at: DateTime.t(),
          organization: Formbricks.Organization.t() | nil,
          creator: Formbricks.User.t() | nil,
          acceptor: Formbricks.User.t() | nil
        }

  schema "Invite" do
    field(:email, :string)
    field(:name, :string)
    field(:expires_at, Formbricks.PrismaDateTime, source: :expiresAt)
    field(:role, Ecto.Enum, values: [:owner, :manager, :member, :billing], default: :member)
    field(:team_ids, {:array, :string}, source: :teamIds)
    field(:organization_id, :string, source: :organizationId)
    field(:creator_id, :string, source: :creatorId)
    field(:acceptor_id, :string, source: :acceptorId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:creator, Formbricks.User, foreign_key: :creator_id, define_field: false, type: :string)
    belongs_to(:acceptor, Formbricks.User, foreign_key: :acceptor_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end