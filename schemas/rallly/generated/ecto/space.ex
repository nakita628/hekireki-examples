defmodule Rallly.Space do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          image: String.t() | nil,
          tier: atom(),
          space_type: atom() | nil,
          industry: String.t() | nil,
          primary_color: String.t() | nil,
          show_branding: boolean(),
          hide_attribution: boolean(),
          shared: boolean(),
          nonprofit_discount_granted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          owner: Rallly.User.t() | nil,
          polls: [Rallly.Poll.t()],
          scheduled_events: [Rallly.ScheduledEvent.t()],
          event_types: [Rallly.EventType.t()],
          sheets: [Rallly.Sheet.t()],
          subscriptions: [Rallly.Subscription.t()],
          members: [Rallly.SpaceMember.t()],
          member_invites: [Rallly.SpaceMemberInvite.t()],
          api_keys: [Rallly.SpaceApiKey.t()],
          nonprofit_applications: [Rallly.NonprofitApplication.t()],
          webhooks: [Rallly.SpaceWebhook.t()]
        }

  schema "spaces" do
    field(:name, :string)
    field(:image, :string)
    field(:tier, Ecto.Enum, values: [:hobby, :pro], default: :hobby)
    field(:space_type, Ecto.Enum, values: [:personal, :work])
    field(:industry, :string)
    field(:primary_color, :string)
    field(:show_branding, :boolean, default: false)
    field(:hide_attribution, :boolean, default: false)
    field(:shared, :boolean, default: false)
    field(:nonprofit_discount_granted_at, Rallly.PrismaDateTime)
    belongs_to(:owner, Rallly.User, foreign_key: :owner_id, type: :string)
    has_many(:polls, Rallly.Poll, foreign_key: :space_id)
    has_many(:scheduled_events, Rallly.ScheduledEvent, foreign_key: :space_id)
    has_many(:event_types, Rallly.EventType, foreign_key: :space_id)
    has_many(:sheets, Rallly.Sheet, foreign_key: :space_id)
    has_many(:subscriptions, Rallly.Subscription, foreign_key: :space_id)
    has_many(:members, Rallly.SpaceMember, foreign_key: :space_id)
    has_many(:member_invites, Rallly.SpaceMemberInvite, foreign_key: :space_id)
    has_many(:api_keys, Rallly.SpaceApiKey, foreign_key: :space_id)
    has_many(:nonprofit_applications, Rallly.NonprofitApplication, foreign_key: :space_id)
    has_many(:webhooks, Rallly.SpaceWebhook, foreign_key: :space_id)
    timestamps(inserted_at_source: :created_at)
  end
end