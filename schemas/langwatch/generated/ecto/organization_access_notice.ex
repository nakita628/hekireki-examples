defmodule LangWatch.OrganizationAccessNotice do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          kind: String.t(),
          email: String.t(),
          name: String.t() | nil,
          idp: String.t() | nil,
          day_key: String.t(),
          idp_refused: boolean(),
          mailed_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "OrganizationAccessNotice" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:kind, :string)
    field(:email, :string)
    field(:name, :string)
    field(:idp, :string)
    field(:day_key, :string, source: :dayKey)
    field(:idp_refused, :boolean, default: false, source: :idpRefused)
    field(:mailed_at, LangWatch.PrismaDateTime, source: :mailedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end