defmodule Ghostfolio.Analytics do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          activity_count: integer(),
          country: String.t() | nil,
          data_provider_ghostfolio_daily_requests: integer(),
          last_request_at: DateTime.t(),
          last_request_client: atom() | nil,
          updated_at: DateTime.t(),
          user: Ghostfolio.User.t() | nil
        }

  schema "Analytics" do
    field(:activity_count, :integer, default: 0, source: :activityCount)
    field(:country, :string)
    field(:data_provider_ghostfolio_daily_requests, :integer, default: 0, source: :dataProviderGhostfolioDailyRequests)
    field(:last_request_at, Ghostfolio.PrismaDateTime, autogenerate: true, source: :lastRequestAt)
    field(:last_request_client, Ecto.Enum, values: [:API, :MCP, :SELF_HOSTING, :WEB], source: :lastRequestClient)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end