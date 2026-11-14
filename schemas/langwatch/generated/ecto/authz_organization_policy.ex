defmodule LangWatch.AuthzOrganizationPolicy do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          braids_organization_admins_only: boolean(),
          updated_by_user_id: String.t(),
          updated_at: DateTime.t()
        }

  schema "AuthzOrganizationPolicy" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:braids_organization_admins_only, :boolean, default: false, source: :braidsOrganizationAdminsOnly)
    field(:updated_by_user_id, :string, source: :updatedByUserId)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end