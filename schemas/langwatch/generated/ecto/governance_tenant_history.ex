defmodule LangWatch.GovernanceTenantHistory do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          tenant_id: String.t(),
          first_used_at: DateTime.t(),
          last_used_at: DateTime.t()
        }

  schema "GovernanceTenantHistory" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:tenant_id, :string, source: :tenantId)
    field(:first_used_at, LangWatch.PrismaDateTime, source: :firstUsedAt)
    field(:last_used_at, LangWatch.PrismaDateTime, source: :lastUsedAt)
  end
end