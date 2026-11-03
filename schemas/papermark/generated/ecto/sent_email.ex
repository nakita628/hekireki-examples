defmodule Papermark.SentEmail do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          type: atom(),
          recipient: String.t(),
          marketing: boolean(),
          domain_slug: String.t() | nil,
          inserted_at: DateTime.t(),
          team: Papermark.Team.t() | nil
        }

  schema "SentEmail" do
    field(:id, :string, primary_key: true)
    field(:type, Ecto.Enum, values: [:FIRST_DAY_DOMAIN_REMINDER_EMAIL, :FIRST_DOMAIN_INVALID_EMAIL, :SECOND_DOMAIN_INVALID_EMAIL, :FIRST_TRIAL_END_REMINDER_EMAIL, :FINAL_TRIAL_END_REMINDER_EMAIL])
    field(:recipient, :string)
    field(:marketing, :boolean, default: false)
    field(:domain_slug, :string, source: :domainSlug)
    field(:team_id, :string, source: :teamId)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end