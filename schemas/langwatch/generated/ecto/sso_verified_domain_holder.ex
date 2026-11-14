defmodule LangWatch.SsoVerifiedDomainHolder do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          domain: String.t(),
          connection_id: String.t(),
          organization_id: String.t()
        }

  schema "SsoVerifiedDomainHolder" do
    field(:domain, :string, primary_key: true)
    field(:connection_id, :string, primary_key: true, source: :connectionId)
    field(:organization_id, :string, source: :organizationId)
  end
end