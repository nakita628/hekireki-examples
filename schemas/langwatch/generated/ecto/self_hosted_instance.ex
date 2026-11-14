defmodule LangWatch.SelfHostedInstance do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          instance_id: String.t(),
          first_seen_at: DateTime.t(),
          last_seen_at: DateTime.t(),
          version: String.t() | nil,
          install_method: String.t() | nil,
          chart_version: String.t() | nil,
          hostname: String.t() | nil,
          environment: String.t() | nil,
          installed_at: DateTime.t() | nil,
          report_schema_version: integer() | nil,
          organization_id: String.t() | nil,
          issued_license_id: String.t() | nil,
          user_email_domains: map() | nil,
          user_domains: [String.t()],
          latest_report: map() | nil,
          optional_metrics_reported: boolean(),
          hostname_reported: boolean(),
          report_count: integer(),
          last_unknown_fields: integer(),
          raised_signals: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "SelfHostedInstance" do
    field(:id, :string, primary_key: true)
    field(:instance_id, :string, source: :instanceId)
    field(:first_seen_at, LangWatch.PrismaDateTime, autogenerate: true, source: :firstSeenAt)
    field(:last_seen_at, LangWatch.PrismaDateTime, source: :lastSeenAt)
    field(:version, :string)
    field(:install_method, :string, source: :installMethod)
    field(:chart_version, :string, source: :chartVersion)
    field(:hostname, :string)
    field(:environment, :string)
    field(:installed_at, LangWatch.PrismaDateTime, source: :installedAt)
    field(:report_schema_version, :integer, source: :reportSchemaVersion)
    field(:organization_id, :string, source: :organizationId)
    field(:issued_license_id, :string, source: :issuedLicenseId)
    field(:user_email_domains, :map, source: :userEmailDomains)
    field(:user_domains, {:array, :string}, source: :userDomains)
    field(:latest_report, :map, source: :latestReport)
    field(:optional_metrics_reported, :boolean, default: true, source: :optionalMetricsReported)
    field(:hostname_reported, :boolean, default: true, source: :hostnameReported)
    field(:report_count, :integer, default: 0, source: :reportCount)
    field(:last_unknown_fields, :integer, default: 0, source: :lastUnknownFields)
    field(:raised_signals, {:array, :string}, source: :raisedSignals)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end