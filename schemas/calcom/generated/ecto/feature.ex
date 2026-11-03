defmodule Calcom.Feature do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          slug: String.t(),
          enabled: boolean(),
          description: String.t() | nil,
          type: atom() | nil,
          stale: boolean() | nil,
          last_used_at: DateTime.t() | nil,
          updated_by: integer() | nil,
          inserted_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          users: [Calcom.UserFeatures.t()],
          teams: [Calcom.TeamFeatures.t()]
        }

  schema "Feature" do
    field(:slug, :string, primary_key: true)
    field(:enabled, :boolean, default: false)
    field(:description, :string)
    field(:type, Ecto.Enum, values: [:RELEASE, :EXPERIMENT, :OPERATIONAL, :KILL_SWITCH, :PERMISSION], default: :RELEASE)
    field(:stale, :boolean, default: false)
    field(:last_used_at, Calcom.PrismaDateTime, source: :lastUsedAt)
    field(:updated_by, :integer, source: :updatedBy)
    has_many(:users, Calcom.UserFeatures, foreign_key: :feature_id, references: :slug)
    has_many(:teams, Calcom.TeamFeatures, foreign_key: :feature_id, references: :slug)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end