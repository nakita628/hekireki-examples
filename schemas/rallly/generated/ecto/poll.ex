defmodule Rallly.Poll do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deadline: DateTime.t() | nil,
          title: String.t(),
          description: String.t() | nil,
          location: String.t() | nil,
          conferencing: map() | nil,
          time_zone: String.t() | nil,
          status: atom(),
          closed_reason: atom() | nil,
          kind: atom(),
          deleted: boolean(),
          deleted_at: DateTime.t() | nil,
          event_id: String.t() | nil,
          hide_participants: boolean(),
          hide_scores: boolean(),
          disable_comments: boolean(),
          require_participant_email: boolean(),
          allow_tentative_votes: boolean(),
          muted: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: Rallly.User.t() | nil,
          scheduled_event: Rallly.ScheduledEvent.t() | nil,
          space: Rallly.Space.t() | nil,
          options: [Rallly.Option.t()],
          participants: [Rallly.Participant.t()],
          comments: [Rallly.Comment.t()],
          votes: [Rallly.Vote.t()],
          invites: [Rallly.PollInvite.t()],
          activities: [Rallly.PollActivity.t()]
        }

  schema "polls" do
    field(:id, :string, primary_key: true)
    field(:deadline, Rallly.PrismaDateTime)
    field(:title, :string)
    field(:description, :string)
    field(:location, :string)
    field(:conferencing, :map)
    field(:time_zone, :string)
    field(:status, Ecto.Enum, values: [:open, :closed, :scheduled, :canceled], default: :open)
    field(:closed_reason, Ecto.Enum, values: [:auto, :manual])
    field(:kind, Ecto.Enum, values: [:date, :time], default: :date)
    field(:deleted, :boolean, default: false)
    field(:deleted_at, Rallly.PrismaDateTime)
    field(:event_id, :string)
    field(:hide_participants, :boolean, default: false)
    field(:hide_scores, :boolean, default: false)
    field(:disable_comments, :boolean, default: true)
    field(:require_participant_email, :boolean, default: false)
    field(:allow_tentative_votes, :boolean, default: true)
    field(:muted, :boolean, default: false)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    belongs_to(:scheduled_event, Rallly.ScheduledEvent, foreign_key: :scheduled_event_id, type: :string)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id, type: :binary_id)
    has_many(:options, Rallly.Option, foreign_key: :poll_id)
    has_many(:participants, Rallly.Participant, foreign_key: :poll_id)
    has_many(:comments, Rallly.Comment, foreign_key: :poll_id)
    has_many(:votes, Rallly.Vote, foreign_key: :poll_id)
    has_many(:invites, Rallly.PollInvite, foreign_key: :poll_id)
    has_many(:activities, Rallly.PollActivity, foreign_key: :poll_id)
    timestamps(inserted_at_source: :created_at)
  end
end