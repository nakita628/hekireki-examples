defmodule Rallly.LicenseValidation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          ip_address: String.t() | nil,
          fingerprint: String.t() | nil,
          validated_at: DateTime.t(),
          user_agent: String.t() | nil,
          license: Rallly.License.t() | nil
        }

  schema "license_validations" do
    field(:id, :string, primary_key: true)
    field(:ip_address, :string)
    field(:fingerprint, :string)
    field(:validated_at, Rallly.PrismaDateTime, autogenerate: true)
    field(:user_agent, :string)
    belongs_to(:license, Rallly.License, foreign_key: :license_id, type: :string)
  end
end