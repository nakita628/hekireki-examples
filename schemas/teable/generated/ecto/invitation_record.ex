defmodule Teable.InvitationRecord do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          invitation_id: String.t(),
          base_id: String.t() | nil,
          space_id: String.t() | nil,
          type: String.t(),
          inviter: String.t(),
          accepter: String.t(),
          created_time: DateTime.t()
        }

  schema "invitation_record" do
    field(:id, :string, primary_key: true)
    field(:invitation_id, :string)
    field(:base_id, :string)
    field(:space_id, :string)
    field(:type, :string)
    field(:inviter, :string)
    field(:accepter, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
  end
end