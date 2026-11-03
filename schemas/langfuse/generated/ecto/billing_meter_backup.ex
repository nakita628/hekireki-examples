defmodule Langfuse.BillingMeterBackup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          stripe_customer_id: String.t(),
          meter_id: String.t(),
          start_time: DateTime.t(),
          end_time: DateTime.t(),
          aggregated_value: integer(),
          event_name: String.t(),
          org_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "billing_meter_backups" do
    field(:stripe_customer_id, :string, primary_key: true)
    field(:meter_id, :string, primary_key: true)
    field(:start_time, Langfuse.PrismaDateTime, primary_key: true)
    field(:end_time, Langfuse.PrismaDateTime, primary_key: true)
    field(:aggregated_value, :integer)
    field(:event_name, :string)
    field(:org_id, :string)
    timestamps(inserted_at_source: :created_at)
  end
end