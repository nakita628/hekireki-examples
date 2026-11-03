defmodule Papermark.ReferralInvite do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          scope_id: String.t(),
          email: String.t(),
          referral_link: String.t() | nil,
          sent_at: DateTime.t(),
          user: Papermark.User.t() | nil,
          team: Papermark.Team.t() | nil
        }

  schema "ReferralInvite" do
    field(:id, :string, primary_key: true)
    field(:scope_id, :string, source: :scopeId)
    field(:email, :string)
    field(:referral_link, :string, source: :referralLink)
    field(:sent_at, Papermark.PrismaDateTime, autogenerate: true, source: :sentAt)
    field(:user_id, :string, source: :userId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:user, Papermark.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
  end
end