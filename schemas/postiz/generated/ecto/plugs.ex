defmodule Postiz.Plugs do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          plug_function: String.t(),
          data: String.t(),
          activated: boolean(),
          integration: Postiz.Integration.t() | nil,
          organization: Postiz.Organization.t() | nil
        }

  schema "Plugs" do
    field(:plug_function, :string, source: :plugFunction)
    field(:data, :string)
    field(:activated, :boolean, default: true)
    field(:integration_id, :string, source: :integrationId)
    field(:organization_id, :binary_id, source: :organizationId)
    belongs_to(:integration, Postiz.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:organization, Postiz.Organization, foreign_key: :organization_id, define_field: false)
  end
end