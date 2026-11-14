defmodule LangWatch.SelfHostedInstanceReport do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          instance_id: String.t(),
          received_at: DateTime.t(),
          version: String.t() | nil,
          report_schema_version: integer() | nil,
          unknown_fields: integer(),
          payload: map()
        }

  schema "SelfHostedInstanceReport" do
    field(:id, :string, primary_key: true)
    field(:instance_id, :string, source: :instanceId)
    field(:received_at, LangWatch.PrismaDateTime, autogenerate: true, source: :receivedAt)
    field(:version, :string)
    field(:report_schema_version, :integer, source: :reportSchemaVersion)
    field(:unknown_fields, :integer, default: 0, source: :unknownFields)
    field(:payload, :map)
  end
end