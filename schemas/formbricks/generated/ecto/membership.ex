defmodule Formbricks.Membership do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          user_id: String.t(),
          accepted: boolean(),
          role: atom(),
          organization: Formbricks.Organization.t() | nil,
          user: Formbricks.User.t() | nil
        }

  schema "Membership" do
    field(:accepted, :boolean, default: false)
    field(:role, Ecto.Enum, values: [:owner, :manager, :member, :billing, :dashboard_viewer], default: :member)
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:user_id, :string, primary_key: true, source: :userId)
    belongs_to(:organization, Formbricks.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:user, Formbricks.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end