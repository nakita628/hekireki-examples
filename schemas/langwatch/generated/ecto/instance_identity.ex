defmodule LangWatch.InstanceIdentity do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          instance_id: String.t(),
          last_report_at: DateTime.t() | nil,
          last_report_error: String.t() | nil,
          optional_metrics_opt_out: boolean(),
          hostname_opt_out: boolean(),
          upgrade_diagnostics_opt_out: boolean(),
          inserted_at: DateTime.t()
        }

  schema "InstanceIdentity" do
    field(:id, :string, primary_key: true, default: "self")
    field(:instance_id, :string, source: :instanceId)
    field(:last_report_at, LangWatch.PrismaDateTime, source: :lastReportAt)
    field(:last_report_error, :string, source: :lastReportError)
    field(:optional_metrics_opt_out, :boolean, default: false, source: :optionalMetricsOptOut)
    field(:hostname_opt_out, :boolean, default: false, source: :hostnameOptOut)
    field(:upgrade_diagnostics_opt_out, :boolean, default: false, source: :upgradeDiagnosticsOptOut)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end