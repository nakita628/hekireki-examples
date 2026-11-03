defmodule Documenso.OrganisationMonthlyStat do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Documenso.PrismaDateTime, autogenerate: {Documenso.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          period: String.t(),
          document_count: integer(),
          email_count: integer(),
          api_count: integer(),
          email_reports: integer(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organisation: Documenso.Organisation.t() | nil
        }

  schema "OrganisationMonthlyStat" do
    field(:id, :string, primary_key: true)
    field(:period, :string)
    field(:document_count, :integer, default: 0, source: :documentCount)
    field(:email_count, :integer, default: 0, source: :emailCount)
    field(:api_count, :integer, default: 0, source: :apiCount)
    field(:email_reports, :integer, default: 0, source: :emailReports)
    field(:organisation_id, :string, source: :organisationId)
    belongs_to(:organisation, Documenso.Organisation, foreign_key: :organisation_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end