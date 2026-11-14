defmodule LangWatch.SeatHold do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          organization_id: String.t(),
          user_id: String.t(),
          email: String.t() | nil,
          name: String.t() | nil,
          seat_taken_at: DateTime.t(),
          held_until: DateTime.t(),
          reason: String.t(),
          inserted_at: DateTime.t()
        }

  schema "SeatHold" do
    field(:id, :string, primary_key: true)
    field(:organization_id, :string, source: :organizationId)
    field(:user_id, :string, source: :userId)
    field(:email, :string)
    field(:name, :string)
    field(:seat_taken_at, LangWatch.PrismaDateTime, source: :seatTakenAt)
    field(:held_until, LangWatch.PrismaDateTime, source: :heldUntil)
    field(:reason, :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end