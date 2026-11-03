defmodule Papermark.InstalledIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          credentials: map() | nil,
          configuration: map() | nil,
          enabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          integration: Papermark.Integration.t() | nil,
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil
        }

  schema "InstalledIntegration" do
    field(:id, :string, primary_key: true)
    field(:credentials, :map)
    field(:configuration, :map)
    field(:enabled, :boolean, default: true)
    field(:integration_id, :string, source: :integrationId)
    field(:user_id, :string, source: :userId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:integration, Papermark.Integration, foreign_key: :integration_id, define_field: false, type: :string)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end