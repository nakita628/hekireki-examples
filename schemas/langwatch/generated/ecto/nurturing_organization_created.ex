defmodule LangWatch.NurturingOrganizationCreated do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          organization_id: String.t(),
          created_at: DateTime.t()
        }

  schema "NurturingOrganizationCreated" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:created_at, LangWatch.PrismaDateTime, source: :createdAt)
  end
end