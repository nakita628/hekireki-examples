defmodule Dub.RegisteredDomain do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Dub.PrismaDateTime, autogenerate: {Dub.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          auto_renewal_disabled_at: DateTime.t() | nil,
          renewal_fee: integer(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Dub.Project.t() | nil,
          domain: Dub.Domain.t() | nil
        }

  schema "RegisteredDomain" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:auto_renewal_disabled_at, Dub.PrismaDateTime, source: :autoRenewalDisabledAt)
    field(:renewal_fee, :integer, default: 1200, source: :renewalFee)
    field(:expires_at, Dub.PrismaDateTime, source: :expiresAt)
    field(:project_id, :string, source: :projectId)
    field(:domain_id, :string, source: :domainId)
    belongs_to(:project, Dub.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:domain, Dub.Domain, foreign_key: :domain_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end