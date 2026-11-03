defmodule Calcom.UserFeatures do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: term(),
          feature_id: String.t(),
          enabled: boolean(),
          assigned_at: DateTime.t(),
          assigned_by: String.t(),
          updated_at: DateTime.t(),
          user: Calcom.User.t() | nil,
          feature: Calcom.Feature.t() | nil
        }

  schema "UserFeatures" do
    field(:enabled, :boolean)
    field(:assigned_at, Calcom.PrismaDateTime, autogenerate: true, source: :assignedAt)
    field(:assigned_by, :string, source: :assignedBy)
    field(:user_id, :id, primary_key: true, source: :userId)
    field(:feature_id, :string, primary_key: true, source: :featureId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    belongs_to(:feature, Calcom.Feature, foreign_key: :feature_id, define_field: false, type: :string, references: :slug)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end