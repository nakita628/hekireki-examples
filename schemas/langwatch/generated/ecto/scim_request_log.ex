defmodule LangWatch.ScimRequestLog do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t() | nil,
          method: String.t(),
          resource: String.t(),
          status: integer(),
          reason: String.t() | nil,
          detail: String.t() | nil,
          occurred_at: DateTime.t(),
          organization: LangWatch.Organization.t() | nil
        }

  schema "ScimRequestLog" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:method, :string)
    field(:resource, :string)
    field(:status, :integer)
    field(:reason, :string)
    field(:detail, :string)
    field(:occurred_at, LangWatch.PrismaDateTime, autogenerate: true, source: :occurredAt)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:organization, LangWatch.Organization, foreign_key: :organization_id, define_field: false, type: :string)
  end
end