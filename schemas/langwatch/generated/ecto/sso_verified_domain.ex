defmodule LangWatch.SsoVerifiedDomain do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          domain: String.t(),
          organization_id: String.t()
        }

  schema "SsoVerifiedDomain" do
    field(:domain, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
  end
end