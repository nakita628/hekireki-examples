defmodule Calcom.ManagedOrganization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          managed_organization_id: term(),
          inserted_at: DateTime.t(),
          managed_organization: Calcom.Team.t() | nil,
          manager_organization: Calcom.Team.t() | nil
        }

  schema "ManagedOrganization" do
    field(:managed_organization_id, :id, primary_key: true, source: :managedOrganizationId)
    field(:manager_organization_id, :id, source: :managerOrganizationId)
    belongs_to(:managed_organization, Calcom.Team, foreign_key: :managed_organization_id, define_field: false)
    belongs_to(:manager_organization, Calcom.Team, foreign_key: :manager_organization_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end