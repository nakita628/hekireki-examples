defmodule Papermark.Domain do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          slug: String.t(),
          verified: boolean(),
          is_default: boolean(),
          redirect_url: String.t() | nil,
          last_checked: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil,
          links: [Papermark.Link.t()]
        }

  schema "Domain" do
    field(:id, :string, primary_key: true)
    field(:slug, :string)
    field(:verified, :boolean, default: false)
    field(:is_default, :boolean, default: false, source: :isDefault)
    field(:redirect_url, :string, source: :redirectUrl)
    field(:last_checked, Papermark.PrismaDateTime, autogenerate: true, source: :lastChecked)
    field(:user_id, :string, source: :userId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:links, Papermark.Link, foreign_key: :domain_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end