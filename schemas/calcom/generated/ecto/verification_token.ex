defmodule Calcom.VerificationToken do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          identifier: String.t(),
          token: String.t(),
          expires: DateTime.t(),
          expires_in_days: integer() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: Calcom.Team.t() | nil,
          secondary_email: Calcom.SecondaryEmail.t() | nil
        }

  schema "VerificationToken" do
    field(:identifier, :string)
    field(:token, :string)
    field(:expires, Calcom.PrismaDateTime)
    field(:expires_in_days, :integer, source: :expiresInDays)
    field(:team_id, :id, source: :teamId)
    field(:secondary_email_id, :id, source: :secondaryEmailId)
    belongs_to(:team, Calcom.Team, foreign_key: :team_id, define_field: false)
    belongs_to(:secondary_email, Calcom.SecondaryEmail, foreign_key: :secondary_email_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end