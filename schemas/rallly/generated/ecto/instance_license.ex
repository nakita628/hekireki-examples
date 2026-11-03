defmodule Rallly.InstanceLicense do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          license_key: String.t(),
          version: integer() | nil,
          type: atom(),
          seats: integer() | nil,
          issued_at: DateTime.t(),
          expires_at: DateTime.t() | nil,
          licensee_email: String.t() | nil,
          licensee_name: String.t() | nil,
          white_label_addon: boolean(),
          status: atom()
        }

  schema "instance_licenses" do
    field(:id, :string, primary_key: true)
    field(:license_key, :string)
    field(:version, :integer)
    field(:type, Ecto.Enum, values: [:PLUS, :ORGANIZATION, :ENTERPRISE])
    field(:seats, :integer)
    field(:issued_at, Rallly.PrismaDateTime, autogenerate: true)
    field(:expires_at, Rallly.PrismaDateTime)
    field(:licensee_email, :string)
    field(:licensee_name, :string)
    field(:white_label_addon, :boolean, default: false)
    field(:status, Ecto.Enum, values: [:ACTIVE, :REVOKED], default: :ACTIVE)
  end
end