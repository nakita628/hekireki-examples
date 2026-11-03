defmodule Papermark.YearInReview do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          team_id: String.t(),
          status: String.t(),
          attempts: integer(),
          last_attempted: DateTime.t() | nil,
          error: String.t() | nil,
          stats: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "YearInReview" do
    field(:id, :string, primary_key: true)
    field(:team_id, :string, source: :teamId)
    field(:status, :string, default: "pending")
    field(:attempts, :integer, default: 0)
    field(:last_attempted, Papermark.PrismaDateTime, source: :lastAttempted)
    field(:error, :string)
    field(:stats, :map)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end